Imports System.Windows.Forms
Imports LibraryToTest
Imports TestFramework
Imports Xunit
Imports Xunit.Extensions

Public Class UnitTest1

  <Theory()>
  <InlineData(False, DialogResult.None, False)>
  <InlineData(True, DialogResult.OK, True)>
  Public Sub TestMethod1(
      ByVal interactive As Boolean,
      ByVal expectedResult As DialogResult,
      ByVal messageShown As Boolean)

    Dim MessageShowCalled As Boolean = False

    Dim th As New TestHarness()

    th.ShowMessageStringString =
        Function(text As String, caption As String)

          Console.WriteLine("Message Box Fake called:")
          Console.WriteLine("text='{0}', caption='{1}'", text, caption)
          MessageShowCalled = True
          Return DialogResult.OK

        End Function

    th.UserInteractiveGet =
        Function() As Boolean

          Console.WriteLine("ShimEnvironment.UserInteractiveGet was called.")
          Return interactive

        End Function

    th.Test =
      Function() As DialogResult
        Return ShowMessage.Message("Test Message", "Test Caption")
      End Function

    Dim result As DialogResult = th.RunTest()

    Assert.Equal(expectedResult, result)
    If messageShown Then
      Assert.True(MessageShowCalled, "The expected message box routine was not called.")
    Else
      Assert.False(MessageShowCalled, "The message box routine was called when it was not expected to.")
    End If

  End Sub

End Class