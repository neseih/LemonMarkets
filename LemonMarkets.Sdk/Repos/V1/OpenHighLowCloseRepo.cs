﻿using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LemonMarkets.Interfaces;
using LemonMarkets.Models;
using LemonMarkets.Models.Enums;
using LemonMarkets.Models.Responses;
using ApiService;
using System;

namespace LemonMarkets.Repos.V1
{

    public class OpenHighLowCloseRepo : IOpenHighLowCloseRepo
    {

        #region vars

        private readonly IApiClient marketApi;

        #endregion vars

        #region ctor

        public OpenHighLowCloseRepo(IApiClient marketApi)
        {
            this.marketApi = marketApi;
        }

        #endregion ctor

        #region methods

        public Task<LemonResults<OHLCEntry>> GetAsync ( OHLCSearchFilter request )
        {
            List<string> param = new List<string>();

            string timeMode = "m1";
            if ( request.TimeMode == OHLCTimeMode.Daily ) timeMode = "d1";
            if ( request.TimeMode == OHLCTimeMode.Hourly ) timeMode = "h1";

            param.Add($"isin={string.Join(',', request.Isins)}");
            if (request.From != null) param.Add($"from={((DateTime)request.From).ToString("yyyy-MM-ddTHH:mm:ss")}");
            if (request.To != null) param.Add($"to={((DateTime)request.To).ToString("yyyy-MM-ddTHH:mm:ss")}");
            if (request.Mic != null) param.Add($"mic={request.Mic}");
            if (request.Sorting != Sorting.None) param.Add($"sorting={request.Sorting}");

            StringBuilder buildParams = new ();
            buildParams.Append("?");
            buildParams.AppendJoin("&", param);

            return this.marketApi.GetAsync<LemonResults<OHLCEntry>> ("ohlc", timeMode, buildParams)!;
        }

        #endregion methods

    }

}