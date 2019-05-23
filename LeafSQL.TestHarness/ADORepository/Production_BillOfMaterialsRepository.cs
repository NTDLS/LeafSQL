using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using LeafSQL.Library.Client;
using LeafSQL.Library.Payloads;

namespace LeafSQL.TestHarness.ADORepository
{
	public partial class Production_BillOfMaterialsRepository
	{        
		public void Export_Production_BillOfMaterials()
		{
            LeafSQLClient client = new LeafSQLClient("http://localhost:6858/");

            if(client.Schema.Exists("AdventureWorks2012:Production:BillOfMaterials"))
			{
				return;
			}

            client.Transaction.Begin();

            client.Schema.Create("AdventureWorks2012:Production:BillOfMaterials");

			using (SqlConnection connection = new SqlConnection("Server=.;Database=AdventureWorks2012;Trusted_Connection=True;"))
			{
				connection.Open();

				try
				{
					using (SqlCommand command = new SqlCommand("SELECT * FROM Production.BillOfMaterials", connection))
					{
						command.CommandTimeout = 10000;
						command.CommandType = System.Data.CommandType.Text;

						using (SqlDataReader dataReader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
						{
                            int indexOfBillOfMaterialsID = dataReader.GetOrdinal("BillOfMaterialsID");
						    int indexOfProductAssemblyID = dataReader.GetOrdinal("ProductAssemblyID");
						    int indexOfComponentID = dataReader.GetOrdinal("ComponentID");
						    int indexOfStartDate = dataReader.GetOrdinal("StartDate");
						    int indexOfEndDate = dataReader.GetOrdinal("EndDate");
						    int indexOfUnitMeasureCode = dataReader.GetOrdinal("UnitMeasureCode");
						    int indexOfBOMLevel = dataReader.GetOrdinal("BOMLevel");
						    int indexOfPerAssemblyQty = dataReader.GetOrdinal("PerAssemblyQty");
						    int indexOfModifiedDate = dataReader.GetOrdinal("ModifiedDate");
						    
							int rowCount = 0;


							while (dataReader.Read() /*&& rowCount++ < 10000*/)
							{
								if(rowCount > 0 && (rowCount % 100) == 0)
								{
									Console.WriteLine("AdventureWorks2012:Production:BillOfMaterials: {0}", rowCount);
								}

								if(rowCount > 0 && (rowCount % 1000) == 0)
								{
									Console.WriteLine("Comitting...");
									client.Transaction.Commit();
									client.Transaction.Begin();
								}

								try
								{
									client.Document.Store("AdventureWorks2012:Production:BillOfMaterials", new Document(new Models.Production_BillOfMaterials
									{
											BillOfMaterialsID= dataReader.GetInt32(indexOfBillOfMaterialsID),
											ProductAssemblyID= dataReader.GetNullableInt32(indexOfProductAssemblyID),
											ComponentID= dataReader.GetInt32(indexOfComponentID),
											StartDate= dataReader.GetDateTime(indexOfStartDate),
											EndDate= dataReader.GetNullableDateTime(indexOfEndDate),
											UnitMeasureCode= dataReader.GetString(indexOfUnitMeasureCode),
											BOMLevel= dataReader.GetInt16(indexOfBOMLevel),
											PerAssemblyQty= dataReader.GetDecimal(indexOfPerAssemblyQty),
											ModifiedDate= dataReader.GetDateTime(indexOfModifiedDate),
										}));
								}
								catch(Exception ex)
								{
									Console.WriteLine(ex.Message);
								}
								
								rowCount++;
							}
						}
					}
					connection.Close();
				}
				catch
				{
					//TODO: add error handling/logging
					throw;
				}

				client.Transaction.Commit();
            }
		}
	}
}

