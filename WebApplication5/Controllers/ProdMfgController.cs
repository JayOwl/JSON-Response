using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class ProdMfgController : ApiController
    {
        // GET: api/ProdMfg
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        // GET: api/ProdMfg/5
        // The parameter for this method must be 'id'. It can be
        // a string or a number.
        public IHttpActionResult Get(string id)
        {
            // Get Manufacturer object by id.
            string mfg = id;
            JB_FoodStoreEntities db = new JB_FoodStoreEntities();
            Manufacturer manufacturer = db.Manufacturers.Where(m => m.mfg == mfg)
                                            .FirstOrDefault();

            // Create JArray of products from anonymous type List.
            var products = db.Products.Where(m => m.mfg == id)
                                .Select(p => new { productID = p.productID, price = p.price })
                                .ToList();
            JArray prodArray = (JArray)JToken.FromObject(products);

            // Create final JSON object.
            dynamic obj = new JObject();
            obj.mfg = manufacturer.mfg;
            obj.mfgDiscount = manufacturer.mfgDiscount;
            obj.products = prodArray;

            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }


        // POST: api/ProdMfg
        // Receives JSON in a format that complies with the ViewModel:
        // // {"manufacturers":
        //    [{"mfg":"Caterpillar","mfgDiscount":20},{"mfg":"Honda","mfgDiscount":10}]}

        // Returns:
        // {"message": "The data was posted successfully!",
        //  "manufacturers": [ {  "name": "Caterpillar" }, { "name": "Honda" }]}
        public IHttpActionResult Post(MfgListVM mfgListVM)
        {
            dynamic obj = new JObject();
            obj.message = "The data was posted successfully!";
            dynamic manufacturers = new JArray();
            foreach (Manufacturer manufacturer in mfgListVM.manufacturers)
            {
                dynamic manufacturerObject = new JObject();
                manufacturerObject.name = manufacturer.mfg;
                manufacturers.Add(manufacturerObject);
            }
            obj.manufacturers = manufacturers;

            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }


        // PUT: api/ProdMfg/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ProdMfg/5
        public void Delete(int id)
        {
        }
    }
}
