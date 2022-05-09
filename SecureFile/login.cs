using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;


namespace SecureFile
{
    public partial class login : UserControl
    {
        public GroupBox g1
        {
            set { this.groupBox1 = value; }
            get { return this.groupBox1; }
        }

        public GroupBox g2
        {
            set { this.groupBox2 = value; }
            get { return this.groupBox2; }
        }

        public Label Hints
        {
            set { this.label2 = value; }
            get { return this.label2; }
        }

        public login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Way safer than string comparison against "BUILTIN\\Administrators"
                IdentityReference BuiltinAdministrators = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);
                IdentityReference AuthenticatedUsers = new SecurityIdentifier(WellKnownSidType.AuthenticatedUserSid, null);

                FileSecurity FileACL = File.GetAccessControl("E:\\USER\\Desktop\\11.png"); // Grab ACL from file

                if (FileACL.GetOwner(typeof(SecurityIdentifier)) != BuiltinAdministrators) // Check if correct owner is set
                {
                    FileACL.SetOwner(BuiltinAdministrators); // If not, make it so!
                }

                foreach (FileSystemAccessRule fsRule in FileACL.GetAccessRules(true, true, typeof(SecurityIdentifier)))
                {
                    if ((fsRule.FileSystemRights & FileSystemRights.Delete) == FileSystemRights.Delete ||
                           (fsRule.FileSystemRights & FileSystemRights.ChangePermissions) == FileSystemRights.ChangePermissions) // Check if rule grants delete or change permissions
                    {
                        FileACL.RemoveAccessRule(fsRule); // If so, nuke it!
                    }
                }

                // Add explicit rules
                FileACL.AddAccessRule(new FileSystemAccessRule(BuiltinAdministrators, FileSystemRights.Delete, AccessControlType.Deny));
                FileACL.AddAccessRule(new FileSystemAccessRule(BuiltinAdministrators, FileSystemRights.Read, AccessControlType.Allow));
                FileACL.AddAccessRule(new FileSystemAccessRule(BuiltinAdministrators, FileSystemRights.ReadAndExecute, AccessControlType.Allow));
                FileACL.AddAccessRule(new FileSystemAccessRule(BuiltinAdministrators, FileSystemRights.Write, AccessControlType.Allow));
                FileACL.AddAccessRule(new FileSystemAccessRule(BuiltinAdministrators, FileSystemRights.Modify, AccessControlType.Allow));
                FileACL.AddAccessRule(new FileSystemAccessRule(BuiltinAdministrators, FileSystemRights.ChangePermissions, AccessControlType.Allow));
                
                FileACL.AddAccessRule(new FileSystemAccessRule(AuthenticatedUsers, FileSystemRights.Delete, AccessControlType.Deny));
                FileACL.AddAccessRule(new FileSystemAccessRule(AuthenticatedUsers, FileSystemRights.ReadAndExecute, AccessControlType.Deny));
                FileACL.AddAccessRule(new FileSystemAccessRule(AuthenticatedUsers, FileSystemRights.Modify, AccessControlType.Deny));
                //FileACL.AddAccessRule(new FileSystemAccessRule(AuthenticatedUsers, FileSystemRights.Delete, AccessControlType.Deny));
                FileACL.AddAccessRule(new FileSystemAccessRule(AuthenticatedUsers, FileSystemRights.ChangePermissions, AccessControlType.Deny));
                FileACL.AddAccessRule(new FileSystemAccessRule(AuthenticatedUsers, FileSystemRights.Read, AccessControlType.Allow));
                FileACL.AddAccessRule(new FileSystemAccessRule(AuthenticatedUsers, FileSystemRights.Write, AccessControlType.Allow));

                FileACL.SetAccessRuleProtection(true, false); // Enable protection from inheritance, remove existing inherited rules
                File.SetAccessControl("E:\\USER\\Desktop\\11.png", FileACL); // Write ACL back to file
            }
            catch
            {
                MessageBox.Show("Error");
            }
            //MessageBox.Show(nameEnDe.EnryptString("rifat"));
            //MessageBox.Show(nameEnDe.DecryptString(nameEnDe.EnryptString("rifat")));
            Form1.Instance.p1.Visible = true;
            Form1.Instance.p2.Visible = true;
            Form1.Instance.p8.Visible = true;

            string[] filePathsn = Directory.GetFiles(@"E:\5.SECURE FOLDER\Encrypted", "*.*",SearchOption.TopDirectoryOnly);
            
             for(int i = 0; i < filePathsn.Length; i++)
             {
                 string destinationPath = Path.Combine(Path.GetDirectoryName(filePathsn[i]), nameEnDe.DecryptString(Path.GetFileName(filePathsn[i])));
                 // Console.WriteLine(filePathsn[i]);
                 //Console.WriteLine(destinationPath);
                 File.Move(filePathsn[i], destinationPath);
             }
             
            string[] filePaths = Directory.GetFiles(@"E:\5.SECURE FOLDER\Encrypted", "*.SFile", SearchOption.TopDirectoryOnly);
            FileList[] f = new FileList[filePaths.Length];



            Form1.Instance.f2mainpanel.Controls.Clear();

            for (int i = 0; i < filePaths.Length; i++)
            {
                
                
                f[i] = new FileList();
                f[i].FileName = Path.GetFileName(filePaths[i]);
                f[i].FilePath = filePaths[i];
                f[i].FileSize = new FileInfo(filePaths[i]).Length.ToString();
                f[i].FileType = "SFile";
                f[i].Operation = "open";
                Form1.Instance.f2mainpanel.Controls.Add(f[i]);
            }

            //if (textBox1.Text == Form1.Instance.data[0])
            //{
            //    Form1.Instance.p1.Visible = true;
            //    Form1.Instance.p2.Visible = true;

            //    string[] filePaths = Directory.GetFiles(@"E:\5.SECURE FOLDER\Encrypted", "*.SFile|*File", SearchOption.TopDirectoryOnly);
            //    FileList[] f = new FileList[filePaths.Length];
                
            //    Form1.Instance.f2mainpanel.Controls.Clear();
                
            //    for (int i = 0; i < filePaths.Length; i++)
            //    {
            //        f[i] = new FileList();
            //        f[i].FileName = Path.GetFileName(filePaths[i]);
            //        f[i].FileSize = filePaths[i].Length;
            //        f[i].FileType = filePaths[i].GetType().ToString();
            //        Form1.Instance.f2mainpanel.Controls.Add(f[i]);
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Incorrect Password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            CommonOpenFileDialog d = new CommonOpenFileDialog();
            d.InitialDirectory = "E:\\USER";
            d.IsFolderPicker = true;
            d.Title = "Select an empty folder.";
            d.Multiselect = false;
            d.ShowDialog();
            try
            {
                string folder = d.FileName;
                MessageBox.Show(folder);
                string filePath = @"E:\USER\Documents\SecureFileData.txt";
                if (!File.Exists(filePath))
                {
                    File.Create(filePath);
                }
                List<string> l = new List<string>();
                l.Add(textBox2.Text);
                l.Add(textBox3.Text);
                l.Add(folder);
                File.WriteAllLines(filePath,l);
                
            }
            catch
            {
                MessageBox.Show("Something went wrong.\nError Code: loginb1c.", "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}
