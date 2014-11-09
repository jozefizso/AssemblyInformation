using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace AssemblyInformation
{
    internal partial class AboutBox : Form
    {
        public AboutBox()
        {
            this.InitializeComponent();
            this.Text = String.Format("About {0}", this.AssemblyTitle);
            this.labelProductName.Text = this.AssemblyProduct;
            this.labelVersion.Text = String.Format("Version {0}", this.AssemblyVersion);
            this.labelCopyright.Text = this.AssemblyCopyright;
            this.labelCompanyName.Text = this.AssemblyCompany;
            this.textBoxDescription.Text = this.AssemblyDescription;
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    var titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }

                return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                return this.GetAssemblyInformation<AssemblyDescriptionAttribute>(attr => attr.Description);
            }
        }

        public string AssemblyProduct
        {
            get
            {
                return this.GetAssemblyInformation<AssemblyProductAttribute>(attr => attr.Product);
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                return this.GetAssemblyInformation<AssemblyCopyrightAttribute>(attr => attr.Copyright);
            }
        }

        public string AssemblyCompany
        {
            get
            {
                return this.GetAssemblyInformation<AssemblyCompanyAttribute>(attr => attr.Company);
            }
        }

        private string GetAssemblyInformation<T>(Func<T, string> valueSelectorFunc) where T : Attribute
        {
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(T), false);
            if (attributes.Length == 0)
            {
                return "";
            }

            return valueSelectorFunc((T)attributes[0]);
        }
        #endregion
    }
}
