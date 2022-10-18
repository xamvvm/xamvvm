using Examples;
using Xamvvm;

namespace ExampleUnitTests.Droid
{
	public class ViewModelUnitTest
	{
		public ViewModelUnitTest()
		{
			var factory = new XamvvmMockFactory();
			XamvvmCore.SetCurrentFactory(factory);
		}

		[Fact]
		public void DetailButtonCommand()
		{
			var mainPageModel = new MainPageModel();

			mainPageModel.DetailButtonCommand.Execute("red");

			Assert.True(XamvvmMockFactory.LastActionSuccess);
			Assert.Equal(XamvvmMockFactory.XammvvmAction.PagePushed, XamvvmMockFactory.LastAction);
			Assert.IsType<DetailPageModel>(XamvvmMockFactory.TargetPageModel);
			Assert.Equal("red", ((DetailPageModel)XamvvmMockFactory.TargetPageModel).Text);
		}
	}
}