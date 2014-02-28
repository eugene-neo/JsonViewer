namespace Opensource.Json.Viewer
{
    public struct ErrorDetails
    {
        internal string _err;
        internal int _pos;

        public string Error
        {
            get
            {
                return this._err;
            }
        }

        public int Position
        {
            get
            {
                return this._pos;
            }
        }

        public void Clear()
        {
            this._err = null;
            this._pos = 0;
        }
    }
}