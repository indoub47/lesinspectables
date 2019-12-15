using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gui.Properties;
using System.IO;

namespace Gui
{
    partial class MainForm
    {


        private string getFileNameFromDialog(string filter, string propName, string fileName = "lesinspectables_output")
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Title = "Išsaugoti failą",
                FileName = fileName,
                Filter = filter,
                FilterIndex = 1
            };

            string settingsOutputDir = Settings.Default[propName].ToString();
            //string propertyValue = Properties.Settings.Default.GetType().GetProperty("OutputDirK40").GetValue(Settings.Default, null).ToString();
            if (settingsOutputDir == null || !Directory.Exists(settingsOutputDir))
            {
                sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            }
            else
            {
                sfd.InitialDirectory = settingsOutputDir;
            }

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Settings.Default[propName] = Path.GetDirectoryName(sfd.FileName);
                Settings.Default.Save();
                return sfd.FileName;
            }
            else
            {
                return "";
            }
        }

        /*
        private string selectFileName(string extensionFilter, string fileName = "lesinspectables_output")
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Title = "Išsaugoti failą",
                FileName = fileName,
                Filter = extensionFilter,
                FilterIndex = 1
            };

            if (Settings.Default.OutputDirToInspect == null || !File.Exists(Settings.Default.OutputDirToInspect))
            {
                sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            }
            else
            {
                sfd.InitialDirectory = Path.GetDirectoryName(Settings.Default.OutputDirToInspect);
            }

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                return sfd.FileName;
            }
            else
            {
                return null;
            }
        }
        */
    }
}
