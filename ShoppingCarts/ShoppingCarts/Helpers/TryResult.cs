using System;

namespace ShoppingCarts.Helpers
{
    public struct TryResult
    {
        public TryResult(Exception e)
        {
            Exception = e ?? throw new ArgumentNullException(nameof(e));
        }

        public bool IsFaulted => Exception != null;

        public Exception Exception { get; }

        public override string ToString()
        {
            return IsFaulted ? Exception.ToString() : "void";
        }
    }

    public struct TryResult<T>
    {
        private readonly T _value;

        private readonly Exception _exception;

        public TryResult(T value)
        {
            _value = value;
            _exception = null;
        }

        public TryResult(Exception e)
        {
            _exception = e ?? throw new ArgumentNullException(nameof(e));
            _value = default(T);
        }

        public static implicit operator TryResult<T>(T value)
        {
            return new TryResult<T>(value);
        }

        public bool IsFaulted => _exception != null;

        public T Value => _value;

        public Exception Exception => _exception;

        public T GetValueOrThrow()
        {
            if (IsFaulted)
                throw Exception;

            return Value;
        }

        public override string ToString()
        {
            return IsFaulted ? _exception.ToString() : Convert.ToString(_value);
        }
    }
}
