using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.QualityTools.Testing.Fakes;
using System.Windows.Forms.Fakes;
using System.Fakes;

namespace TestFramework
{
    public class TestHarness
    {
        public FakesDelegates.Func<string, string, DialogResult> ShowMessageStringString { private get; set; }

        public FakesDelegates.Func<bool> UserInteractiveGet { private get; set; }

        public FakesDelegates.Func<DialogResult> Test { private get; set; }

        public DialogResult RunTest()
        {
            using (ShimsContext.Create())
            {
                DialogResult result = DialogResult.Abort;
                if (ShowMessageStringString != null)
                    ShimMessageBox.ShowStringString = ShowMessageStringString;

                if (UserInteractiveGet != null)
                    ShimEnvironment.UserInteractiveGet = UserInteractiveGet;

                if (Test != null)
                    result = Test();

                return result;
            }

        }

    }
}
