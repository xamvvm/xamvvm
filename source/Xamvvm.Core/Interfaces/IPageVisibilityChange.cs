namespace Xamvvm
{
    public interface IPageVisibilityChange
    {
        /// <summary>
        /// Called when page is appearing.
        /// </summary>
        void OnAppearing();


        /// <summary>
        /// Called when page is disappearing.
        /// </summary>
        void OnDisappearing();
    }
}
