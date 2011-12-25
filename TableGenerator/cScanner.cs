using System;
using System.Collections.Generic;
using System.Text;

namespace TableGenerator
{
    public abstract class cScanner: IDisposable
    {
        public abstract cToken cm_GetNextToken();

        #region Члены IDisposable

        public virtual void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
