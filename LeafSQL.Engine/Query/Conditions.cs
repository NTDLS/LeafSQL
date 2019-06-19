using LeafSQL.Engine.Exceptions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using static LeafSQL.Engine.Constants;

namespace LeafSQL.Engine.Query
{
    public class Conditions
    {
        public List<Condition> Root = new List<Condition>();
        public List<Condition> _flattened = null;

        public int Count
        {
            get
            {
                return Root.Count;
            }
        }

        /// <summary>
        /// Returns a flattened list of all nexted conditons.
        /// </summary>
        public List<Condition> Flattened
        {
            get
            {
                if (_flattened == null)
                {
                    _flattened = new List<Condition>();
                    GetFlattenedConditions(this, ref _flattened);
                }
                return _flattened;
            }
        }

        private void GetFlattenedConditions(Conditions conditions, ref List<Condition> outConditions)
        {
            outConditions.AddRange(conditions.Root);

            if (conditions.Nest != null)
            {
                foreach (var nestedConditions in conditions.Nest)
                {
                    GetFlattenedConditions(nestedConditions, ref outConditions);
                }
            }
        }

        private List<Conditions> _Nest = null;

        public List<Conditions> Nest
        {
            get
            {
                if (_Nest == null)
                {
                    _Nest = new List<Conditions>();
                }
                return _Nest;
            }
            set
            {
                _Nest = value;
            }
        }

        public ConditionType ConditionType { get; set; }

        public bool LowerCased { get; set; }

        public void MakeLowerCase()
        {
            if (LowerCased == false)
            {
                LowerCased = true;
                foreach (Condition condition in Root)
                {
                    condition.Key = condition.Key.ToLower();
                    condition.Value = condition.Value.ToLower();
                }

                if (_Nest != null)
                {
                    foreach (Conditions nestedConditions in _Nest)
                    {
                        nestedConditions.MakeLowerCase();
                    }
                }
            }
        }

        public Conditions()
        {
        }

        public void Add(Conditions conditions)
        {
            this.Nest = conditions.Nest;
            foreach (Condition condition in conditions.Root)
            {
                this.Add(condition);
            }
        }

        public void Add(ConditionType conditionType, string key, ConditionQualifier conditionQualifier, string value)
        {
            this.Root.Add(new Condition(conditionType, key.ToLower(), conditionQualifier, value.ToLower()));
        }

        public void Add(Condition condition)
        {
            this.Add(condition.ConditionType, condition.Key, condition.ConditionQualifier, condition.Value);
        }

        public bool IsMatch(Conditions conditions, JObject jsonContent)
        {
            bool fullAttributeMatch = true;

            foreach (Condition condition in conditions.Root)
            {
                JToken jToken = null;

                if (jsonContent.TryGetValue(condition.Key, StringComparison.CurrentCultureIgnoreCase, out jToken))
                {
                    string jValue = jToken.ToString().ToLower();

                    if (condition.ConditionType == ConditionType.None) //"None" is the first condition.
                    {
                        fullAttributeMatch = condition.IsMatch(jValue);
                    }
                    else if (condition.ConditionType == ConditionType.And)
                    {
                        fullAttributeMatch = fullAttributeMatch && condition.IsMatch(jValue);
                    }
                    else if (condition.ConditionType == ConditionType.Or)
                    {
                        fullAttributeMatch = fullAttributeMatch || condition.IsMatch(jValue);
                    }
                    else
                    {
                        throw new LeafSQLExceptionBase("Unsupported expression type.");
                    }
                }
            }

            if (conditions.Nest != null)
            {
                foreach (var nestedCondition in conditions.Nest)
                {
                    if (nestedCondition.ConditionType == ConditionType.And)
                    {
                        fullAttributeMatch = fullAttributeMatch && nestedCondition.IsMatch(jsonContent);
                    }
                    else if (nestedCondition.ConditionType == ConditionType.Or)
                    {
                        fullAttributeMatch = fullAttributeMatch || nestedCondition.IsMatch(jsonContent);
                    }
                    else
                    {
                        throw new LeafSQLExceptionBase("Unsupported nested expression type.");
                    }
                }
            }

            return fullAttributeMatch;
        }

        public bool IsMatch(JObject jsonContent)
        {
            return IsMatch(this, jsonContent);
        }
    }
}