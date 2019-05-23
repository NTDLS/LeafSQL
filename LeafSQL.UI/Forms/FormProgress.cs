using System;
using System.Windows.Forms;

namespace LeafSQL.UI.Forms
{
    public partial class FormProgress : Form
    {
        #region Events

        public class OnCancelInfo
        {
            public bool Cancel = false;
        }

        public delegate void EventOnCancel(Object sender, OnCancelInfo e);
        public event EventOnCancel OnCancel;

        #endregion

        #region Static Methods

        private static FormProgress staticForm;
        public static FormProgress Instance
        {
            get
            {
                if (staticForm == null)
                {
                    staticForm = new FormProgress();
                }
                return staticForm;
            }
            set
            {
                staticForm = value;
            }
        }

        public static object StaticResultObject
        {
            get
            {
                return Instance.ResultObject;
            }
        }
        
        public static void Complete(DialogResult dialogResult)
        {
            Instance.CloseFormWithResult(dialogResult);
        }

        public static void Complete(object resultObject)
        {
            Instance.ResultObject = resultObject;
            Instance.CloseFormWithResult(DialogResult.OK);
        }

        public static void Complete(object resultObject, DialogResult dialogResult)
        {
            Instance.ResultObject = resultObject;
            Instance.CloseFormWithResult(dialogResult);
        }

        public static void Complete()
        {
            Instance.CloseFormWithResult(DialogResult.OK);
        }

        public static void WaitForVisible()
        {
            while (Instance == null || Instance.Visible == false)
            {
                System.Threading.Thread.Sleep(10);
            }
        }

        public static DialogResult Start(string headerText)
        {
            /*
            if (Instance != null)
            {
                Instance.Dispose();
                Instance = null;
            }
            Instance = new FormProgress();
            */
            
            Instance.HeaderText = headerText;

            return Instance.ShowDialog();
        }

        public static void Reset()
        {

            Instance.lblHeader.Text = "Please wait...";
            Instance.lblBody.Text = "";
            Instance.cmdCancel.Enabled = false;
            Instance.pbProgress.Minimum = 0;
            Instance.pbProgress.Maximum = 100;
            Instance.DialogResult = DialogResult.OK;
        }

        #endregion

        public object ResultObject { get; set; }

        public FormProgress()
        {
            InitializeComponent();

            lblHeader.Text = "Please wait...";
            lblBody.Text = "";
            cmdCancel.Enabled = false;
            pbProgress.Minimum = 0;
            pbProgress.Maximum = 100;

            this.DialogResult = DialogResult.OK;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            if (OnCancel != null)
            {
                OnCancelInfo onCancelInfo = new OnCancelInfo();
                OnCancel(this, onCancelInfo);
                if(onCancelInfo.Cancel)
                {
                    return;
                }
            }

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #region Properties.
        public bool CanCancel
        {
            get { return cmdCancel.Enabled; }
            set { cmdCancel.Enabled = value; }
        }
        public string CaptionText
        {
            get { return this.Text; }
            set { this.Text = value; }
        }
        public string HeaderText
        {
            get { return this.lblHeader.Text; }
            set { this.lblHeader.Text = value; }
        }
        public string BodyText
        {
            get { return this.lblBody.Text; }
            set { this.lblBody.Text = value; }
        }
        public int ProgressMinimum
        {
            get { return pbProgress.Minimum; }
            set { pbProgress.Minimum = value; }
        }
        public int ProgressMaximum
        {
            get { return pbProgress.Maximum; }
            set { pbProgress.Maximum = value; }
        }
        public int ProgressPosition
        {
            get { return pbProgress.Value; }
            set
            {
                if (ProgressStyle == ProgressBarStyle.Marquee)
                {
                    ProgressStyle = ProgressBarStyle.Continuous;
                }

                pbProgress.Value = value;
            }
        }
        public ProgressBarStyle ProgressStyle
        {
            get { return pbProgress.Style; }
            set { pbProgress.Style = value; }
        }
        #endregion

        public void CloseFormWithResult(DialogResult dialogResult)
        {
            this.DialogResult = dialogResult;
            this.Close();
        }
    }
}
