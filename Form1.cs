using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace V2MemInjector
{
    public partial class V2MemInjector : Form
    {
        private string settingsFile;
        public V2MemInjector()
        {
            InitializeComponent();
            clistModList.Items.Clear();

            // Used to remember mod path between launches
            settingsFile = AppDomain.CurrentDomain.BaseDirectory + "path.txt";
            try
            {
                if (!File.Exists(settingsFile))
                {
                    File.Create(settingsFile).Close();
                }
                else
                {
                    string modPathInSettings = File.ReadAllText(settingsFile);
                    if (Directory.Exists(modPathInSettings))
                    {
                        modPath = modPathInSettings;
                    }
                    GetMods();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            Injector.FindProgram();
        }

        private string modPath = null;
        private void btnDLLFolder_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                modPath = folderBrowserDialog1.SelectedPath;
                try
                {
                    if (File.Exists(settingsFile))
                    {
                        File.WriteAllText(settingsFile, modPath);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return;
                }
                GetMods();
            }
            else
            {
                MessageBox.Show("Error in selecting path!");
            }
        }

        private string[] mods = { "No mods" };
        private void GetMods()
        {
            try
            {
                if (modPath != null)
                {
                    mods = Directory.GetFiles(modPath, "*.dll", SearchOption.AllDirectories);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            if (mods[0] != "No mods")
            {
                clistModList.Items.Clear();
                foreach (string mod in mods)
                {
                    clistModList.Items.Add(Path.GetFileName(mod), false);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            GetMods();
        }

        private void btnInject_Click(object sender, EventArgs e)
        {
            string[] activatedMods = new string[0];
            foreach (object itemChecked in clistModList.CheckedItems)
            {
                string itemCheckedStr = itemChecked.ToString();
                string realModPath = modPath + "\\" + itemCheckedStr;
                Injector.Inject(realModPath);
            }
        }
    }
}
