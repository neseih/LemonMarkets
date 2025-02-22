﻿using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LemonMarkets.Interfaces;
using LemonMarkets.Models;
using LemonMarkets.Models.Responses;
using ApiService;

namespace LemonMarkets.Repos.V1
{

    public class VenuesRepo : IVenuesRepo
    {

        #region vars

        private readonly IApiClient marketApi;

        #endregion vars

        #region ctor

        public VenuesRepo(IApiClient marketApi)
        {
            this.marketApi = marketApi;
        }

        #endregion ctor

        #region methods

        public Task<LemonResults<Venue>> GetAsync ( VenueSearchFilter? request = null )
        {
            if (request == null) return this.marketApi.GetAsync<LemonResults<Venue>> ("venues")!;

            List<string> param = new List<string>();

            if (request.Mic != null) param.Add($"mic={request.Mic}");

            if (param.Count == 0) return this.marketApi.GetAsync<LemonResults<Venue>> ("venues")!;

            StringBuilder buildParams = new ();
            buildParams.Append("?");
            buildParams.AppendJoin("&", param);

            return this.marketApi.GetAsync<LemonResults<Venue>> ("venues", buildParams)!;
        }

        #endregion methods

    }

}