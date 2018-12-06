﻿using FeedAPI.Services.Base;
using FeedAPI.Common;
using FeedAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace FeedAPI.Services
{
    public class FeedsService : IApiService
    {
        private Rss _result = null;
        private StringOrganizer _stringOrganizer;

        public FeedsService()
        {
            this._stringOrganizer = new StringOrganizer();
        }

        public void Load()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Rss));

            try
            {
                _result = (Rss)serializer.Deserialize(new XmlTextReader(@"https://www.minutoseguros.com.br/blog/feed/"));
            }
            catch (XmlException xmlex)
            {
                throw new Exception("Não foi possível ler os feeds", new Exception(xmlex.Message));
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível ler os feeds", new Exception(ex.Message));
            }
        }

        public IActionResult GetTop(int quantity)
        {
            if (this._result == null)
                return new BadRequestObjectResult(new Exception("Feed não foi carregado corretamente"));

            foreach (Item item in this._result.Channel.Item)
            {
                this._stringOrganizer.ClearTemp();

                this._stringOrganizer.CountWords(item.Title);
                this._stringOrganizer.CountWords(item.Encoded);
                this._stringOrganizer.CountWords(item.Description);

                item.WordsCount = this._stringOrganizer.GetTempWordsSum();
            }

            return new JsonResult(_result.Channel.Item.Take(quantity).ToList());
        }

        public IActionResult Get()
        {
            if (this._result == null)
                return new BadRequestObjectResult(new Exception("Feed não foi carregado corretamente"));

            foreach (Item item in this._result.Channel.Item)
            {
                this._stringOrganizer.ClearTemp();

                this._stringOrganizer.CountWords(item.Title);
                this._stringOrganizer.CountWords(item.Encoded);
                this._stringOrganizer.CountWords(item.Description);

                item.WordsCount = this._stringOrganizer.GetTempWordsSum();
            }

            return new JsonResult(_result.Channel.Item);
        }

        #region IDisposable Support

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (this._stringOrganizer != null)
                    {
                        this._stringOrganizer.Dispose();
                        this._stringOrganizer = null;
                    }

                    this._result = null;
                }
                
                disposedValue = true;
            }
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
