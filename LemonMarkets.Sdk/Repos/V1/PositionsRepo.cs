﻿using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using LemonMarkets.Models;
using LemonMarkets.Models.Responses;

using ApiService;

using LemonMarkets.Models.Filters;
using LemonMarkets.Interfaces;

namespace LemonMarkets.Repos.V1
{

    public class PositionsRepo : IPositionsRepo
    {

        #region vars

        private readonly IApiClient tradingApi;

        #endregion vars

        #region ctor

        public PositionsRepo ( IApiClient tradingApi )
        {
            this.tradingApi = tradingApi;
        }

        #endregion ctor

        #region methods

        public Task<LemonResults<PositionEntry>> GetAsync(PositionSearchFilter? filter = null)
        {
            if ( filter is null ) return this.tradingApi.GetAsync<LemonResults<PositionEntry>> ("positions")!;

            List<string> param = new List<string>();

            if (filter.Isins.Count != 0) param.Add($"isin={string.Join(',', filter.Isins)}");

            if (param.Count == 0) return this.tradingApi.GetAsync<LemonResults<PositionEntry>> ("positions")!;

            StringBuilder buildParams = new ();
            buildParams.Append("?");
            buildParams.AppendJoin("&", param);

            return this.tradingApi.GetAsync<LemonResults<PositionEntry>> ("positions", buildParams)!;
        }

        #endregion methods

    }

}