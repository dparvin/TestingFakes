using System;
using System.Windows.Forms;

namespace LibraryToTest
{
    public class ShowMessage
    {
        public static DialogResult Message(string Msg, string Caption)
        {
            if (Environment.UserInteractive)
                return MessageBox.Show(Msg, Caption);
            else
            {
                Console.WriteLine(Caption + "\n" + Msg);
                return DialogResult.None;
            }
        }
    }
}
