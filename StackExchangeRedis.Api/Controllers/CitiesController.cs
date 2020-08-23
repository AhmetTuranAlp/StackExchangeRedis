using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StackExchangeRedis.Api.Common;
using StackExchangeRedis.Api.Model;
using StackExchangeRedis.Cache.CacheService;

namespace StackExchangeRedis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private ICacheService _cacheService;
        public CitiesController(ICacheService cacheService)
        {
            _cacheService = cacheService;

            if (!_cacheService.Any("turkey"))
            {
                _cacheService.Add("turkey", GetCityList("turkey"), 5);
            }
        }

        /// <summary>
        /// Ülke kodu ile şehirler getirilmektedir.
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        [HttpGet, Route("[action]/countryName={countryName}")]
        public ListResponse<City> GetCountry(string countryName)
        {
            try
            {
                if (!string.IsNullOrEmpty(countryName))
                {
                    if (_cacheService.Any(countryName))
                    {
                        List<City> cities = _cacheService.Get<List<City>>(countryName);
                        if (cities != null)
                            return new ListResponse<City>() { IsSuccess = true, Message = "Transaction Successful.", Result = cities, TotalCount = cities.Count(), Exception = null };
                        else
                            return new ListResponse<City>() { IsSuccess = false, Message = "No Registration For This Country.", Result = new List<City>(), TotalCount = 0, Exception = null };
                    }
                    else
                    {
                        return new ListResponse<City>() { IsSuccess = false, Message = "No Registration For This Country.", Result = new List<City>(), TotalCount = 0, Exception = null };
                    }
                }
                else
                {
                    return new ListResponse<City>() { IsSuccess = false, Message = "Parameter Null", Result = new List<City>(), TotalCount = 0, Exception = null };
                }
            }
            catch (Exception ex)
            {
                return new ListResponse<City>() { IsSuccess = false, Message = "An Error Occurred.", Result = new List<City>(), TotalCount = 0, Exception = ex };
            }
        }

        /// <summary>
        /// Ülke kodu ve şehir kodu ile şehir getirilmektedir.
        /// </summary>
        /// <param name="countryId"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        [HttpGet, Route("[action]/countryName={countryName}&cityId={cityId}")]
        public Response<City> GetCity(string countryName, string cityId)
        {
            try
            {
                if (!string.IsNullOrEmpty(countryName) && !string.IsNullOrEmpty(cityId))
                {
                    if (_cacheService.Any(countryName))
                    {
                        List<City> cities = _cacheService.Get<List<City>>(countryName);
                        if (cities != null)
                        {
                            City city = cities.FirstOrDefault(x => x.CityId == Convert.ToInt32(cityId));
                            if (city != null)
                                return new Response<City>() { IsSuccess = true, Message = "Transaction Successful.", Result = city, Exception = null };
                            else
                                return new Response<City>() { IsSuccess = false, Message = "No Registration For This City.", Result = new City(), Exception = null };
                        }
                        else
                        {
                            return new Response<City>() { IsSuccess = false, Message = "Parameter Null", Result = new City(), Exception = null };
                        }
                    }
                    else
                    {
                        return new Response<City>() { IsSuccess = false, Message = "Parameter Null", Result = new City(), Exception = null };
                    }
                }
                else
                {
                    return new Response<City>() { IsSuccess = false, Message = "Parameter Null", Result = new City(), Exception = null };
                }
            }
            catch (Exception ex)
            {
                return new Response<City>() { IsSuccess = false, Message = "An Error Occurred.", Result = new City(), Exception = ex };
            }
        }

        /// <summary>
        /// Ülke kodu ve şehir modeli ile şehir eklemesi yapılmaktadır.
        /// </summary>
        /// <param name="city"></param>
        /// <param name="countryId"></param>
        /// <returns></returns>
        [HttpPost, Route("[action]")]
        public ListResponse<City> SetCity([FromBody]City city, string countryName)
        {
            try
            {
                if (city != null && !string.IsNullOrEmpty(countryName))
                {
                    if (_cacheService.Any(countryName))
                    {
                        List<City> cities = _cacheService.Get<List<City>>(countryName);
                        if (cities == null)
                            cities = new List<City>();

                        cities.Add(city);
                        _cacheService.Add(countryName, cities, 5);
                        return new ListResponse<City>() { IsSuccess = true, Message = "Transaction Successful", Result = cities, TotalCount = cities.Count(), Exception = null };
                    }
                    else
                    {
                        return new ListResponse<City>() { IsSuccess = false, Message = "Parameter Null", Result = new List<City>(), TotalCount = 0, Exception = null };
                    }
                }
                else
                {
                    return new ListResponse<City>() { IsSuccess = false, Message = "Parameter Null", Result = new List<City>(), TotalCount = 0, Exception = null };
                }
            }
            catch (Exception ex)
            {
                return new ListResponse<City>() { IsSuccess = false, Message = "An Error Occurred.", Result = new List<City>(), TotalCount = 0, Exception = ex };
            }
        }

        /// <summary>
        /// Ülke koduna göre şehirleri dönmektedir.
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        public List<City> GetCityList(string countryId)
        {
            List<City> cities = new List<City>();
            if (countryId == "turkey")
            {
                cities.Add(new City { CityId = 34, CityName = "İstanbul" });
                cities.Add(new City { CityId = 58, CityName = "Sivas" });
                cities.Add(new City { CityId = 54, CityName = "Sakarya" });
                cities.Add(new City { CityId = 06, CityName = "Ankara" });
            }
            return cities;
        }
    }
}