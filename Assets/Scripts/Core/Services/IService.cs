namespace Core.Services
{
    public interface IService
    {
        /// <summary>
        /// Called when the service is started (i.e first called)
        /// </summary>
        void OnStart();
        
        /// <summary>
        /// Called when the service is ended (i.e. game quit)
        /// </summary>
        void OnEnd();
    }
}