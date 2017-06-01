using LibraryToTest;
using System.Windows.Forms;
using TestFramework;
using Xunit;
using Xunit.Abstractions;

namespace UnitTests
{
    public class UnitTest1
    {
        private ITestOutputHelper Output { get; }

        public UnitTest1(ITestOutputHelper outputHelper)
        {
            Output = outputHelper;
        }

        [Theory()]
        [InlineData(false, DialogResult.None, false)]
        [InlineData(true, DialogResult.OK, true)]
        public void TestMethod1(bool interactive, DialogResult expectedResult, bool messageShown)
        {
            bool MessageShowCalled = false;
            var th = new TestHarness();

            th.ShowMessageStringString = (text, caption) =>
            {
                Output.WriteLine("Message Box Fake called:");
                Output.WriteLine("text='{0}', caption='{1}'", text, caption);
                MessageShowCalled = true;
                return DialogResult.OK;
            };

            th.UserInteractiveGet = () =>
            {
                Output.WriteLine("ShimEnvironment.UserInteractiveGet was called.");
                return interactive;
            };

            th.Test = () =>
            {
                return ShowMessage.Message("Test Message", "Test Caption");
            };

            var result = th.RunTest();

            Assert.Equal(expectedResult, result);
            if (messageShown)
            {
                Assert.True(MessageShowCalled, "The expected message box routine was not called.");
            }
            else
            {
                Assert.False(MessageShowCalled, "The message box routine was called when it was not expected to.");
            }
        }
    }
}
