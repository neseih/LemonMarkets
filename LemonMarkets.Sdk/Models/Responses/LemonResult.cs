﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiService;

namespace LemonMarkets.Models.Responses
{
    public class LemonResult : IApiResponse
    {

        private bool? success;

        #region get/set

        /// <summary>
        /// Timestamp of your API request
        /// </summary>
        public DateTime Time
        {
            get;
            set;
        }

        /// <summary>
        /// API returns "ok" when account was successfully retrieved.
        /// </summary>
        public string? Status
        {
            get;
            set;
        }

        /// <summary>
        /// Die Error codes können in der Doku nachgeschaut werden:
        /// https://docs.lemon.markets/error-handling
        /// </summary>
        public string? Error_code
        {
            get;
            set;
        }

        public string? Error_message
        {
            get;
            set;
        }

        /// <summary>
        /// Environment the request was placed in: "paper" or "money"
        /// </summary>
        public string? Mode
        {
            get;
            set;
        }

        [Obsolete("please use IsSuccess")]
        public bool IsOk
        {
            get
            {
                return this.IsSuccess;
            }
        }

        public int HttpCode
        {
            get;
            set;
        }

        public virtual bool IsSuccess
        {
            get
            {
                if (this.Exception is not null) return false;

                if (this.success is not null && !(bool)this.success) return (bool)this.success;

                return this.Status == "ok";
            }
        }

        public Exception? Exception
        {
            get;
            private set;
        }

        #endregion get/set

        #region ctor

        public LemonResult()
        {

        }

        public LemonResult(string status)
        {
            this.Status = status;
        }

        #endregion ctor

        #region methods

        public bool SetHttpCode(int httpCode)
        {
            this.HttpCode = httpCode;

            return true;
        }

        public bool SetIsSuccess(bool isSuccess)
        {
            this.success = isSuccess;

            return true;
        }

        public bool SetStatus(string status)
        {
            if (string.IsNullOrEmpty(this.Status)) this.Status = status;

            return true;
        }

        public bool SetException(Exception exception)
        {
            this.Exception = exception;

            return true;
        }

        #endregion methods

    }
}
