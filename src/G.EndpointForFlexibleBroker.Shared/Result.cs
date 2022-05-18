using System;
using System.Collections.Generic;

namespace G.EndpointForFlexibleBroker.Shared
{
    /// <summary>
    /// Result of operation
    /// based on ref: https://josef.codes/my-take-on-the-result-class-in-c-sharp/
    /// </summary>
    public abstract class Result
    {
        public bool Success { get; protected set; }
        public bool Failure => !Success;
    }

    public abstract class Result<T> : Result
    {
        private T _data;

        protected Result(T data)
        {
            Data = data;
        }

        public T Data
        {
            get => Success ? _data : throw new Exception($"You can't access .{nameof(Data)} when .{nameof(Success)} is false");
            set => _data = value;
        }
    }

    public class SuccessResult : Result
    {
        public SuccessResult()
        {
            Success = true;
        }
    }

    public class SuccessResult<T> : Result<T>
    {


        public SuccessResult(T data) : base(data)
        {
            Success = true;
        }
    }

    public class ErrorResult : Result, IErrorResult
    {
        public ErrorResult(string message) : this(message, Array.Empty<Error>())
        {
        }

        public ErrorResult(Exception ex) : this(ex.ToString(), Array.Empty<Error>())
        {
        }

        public ErrorResult(string message, IReadOnlyCollection<Error> errors)
        {
            Message = message;
            Success = false;
            Errors = errors ?? Array.Empty<Error>();
        }

        public string Message { get; }
        public IReadOnlyCollection<Error> Errors { get; }

        public override string ToString()
        {
            return $"{nameof(ErrorResult)}: {Message} {string.Join(";", Errors)}";
        }
    }

    public class ErrorResult<T> : Result<T>, IErrorResult
    {
        public ErrorResult(string message) : this(message, Array.Empty<Error>())
        {
        }

        public ErrorResult(Exception ex) : this(ex.ToString(), Array.Empty<Error>())
        {
        }

        public ErrorResult(string message, IReadOnlyCollection<Error> errors) : base(default)
        {
            Message = message;
            Success = false;
            Errors = errors ?? Array.Empty<Error>();
        }

        public string Message { get; set; }
        public IReadOnlyCollection<Error> Errors { get; }

        public override string ToString()
        {
            return $"{nameof(ErrorResult<T>)}: {Message} {string.Join(";", Errors)}";
        }
    }

    public class Error
    {
        public Error(string details) : this(null, details)
        {

        }

        public Error(string code, string details)
        {
            Code = code;
            Details = details;
        }

        public string Code { get; }
        public string Details { get; }

        public override string ToString()
        {
            return $"{Code}:{Details}";
        }
    }

    internal interface IErrorResult
    {
        string Message { get; }
        IReadOnlyCollection<Error> Errors { get; }
    }

    public class NotFoundResult<T> : ErrorResult<T>
    {
        public NotFoundResult(string propertyName, string message) : base(message)
        {
            PropertyName = propertyName;
        }

        public string PropertyName { get; }

        public override string ToString()
        {
            return $"{nameof(NotFoundResult<T>)}({PropertyName}): {Message} {string.Join(";", Errors)}";
        }
    }
}