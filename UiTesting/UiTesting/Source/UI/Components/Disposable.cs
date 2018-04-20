using System;

namespace UiTesting.Source.UI.Components
{
    public class Disposable : IDisposable
    {
        #region Fields
    
        private static int count = 0;

        #endregion

        #region Properties

        public static int Count { get { return count; } }

        #endregion

        #region Constructors

        protected Disposable()
        {
            count += 1;
        }

        #endregion

        #region Destructors

        ~Disposable()
        {
            Dispose(false);
        }
	  	  
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        } 	  	  
 	  	  
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                count -= 1;
            }
        }	  	  	  	 

        #endregion
    }
}
