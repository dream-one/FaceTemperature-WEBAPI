using FaceServer.Models;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace FaceServer.Tools {
    public static class ControllerExtention {
        /// <summary>
        /// 发送成功数据
        /// </summary>
        /// <param name="controller">控制器</param>
        /// <param name="data">需要发送给前端的数据</param>
        /// <returns></returns>
        public static OkNegotiatedContentResult<ResponseData> SendData(this ApiController controller, object data) {
            return new OkNegotiatedContentResult<ResponseData>(new ResponseData() {
                Data = data
            }, controller);
        }
        /// <summary>
        /// 发送失败数据
        /// </summary>
        /// <param name="controller">控制器</param>
        /// <param name="error">错误信息</param>
        /// <param name="code">错误码,默认500</param>
        /// <returns></returns>
        public static OkNegotiatedContentResult<ResponseData> ErrorData(this ApiController controller, string error, int code = 500) {
            return new OkNegotiatedContentResult<ResponseData>(new ResponseData() {
                Code = code,
                ErrMessage = error
            }, controller);
        }
    }

    public class JwtTools {
        public static string key = "yixiangkeji";


        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="payload">明文可见的东西</param>
        /// 
        /// <returns></returns>
        public static string Encode(Dictionary<string, string> payload) {
            var sercret = key;
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            //添加令牌时间，默认是现在，即登录的时候
            payload.Add("timeout", DateTime.Now.AddDays(1).ToString());
            return encoder.Encode(payload, sercret);//返回token
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="token">token令牌</param>
        /// <param name="key">秘钥</param>
        /// <returns>登录信息对象</returns>
        public static Dictionary<string, string> DEcode(string token) {
            var secret = key;
            try {
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);
                //首先将token转换成json
                var json = decoder.Decode(token, secret, true);
                //然后转换成键值对对象
                var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                //判断登录是否过期，过期则报错，未过期则移除登录时间属性。
                if (Convert.ToDateTime(result["timeout"])
< DateTime.Now) {
                    throw new Exception("登录失效，请重新登录");
                }
                result.Remove("timeout");
                //返回键值对对象
                return result;
            }
            catch (Exception ex) {
               throw new Exception(ex.Message);
            }
           
            
        }
        public static Dictionary<string, string> ValideLogined(HttpRequestHeaders headers) {
            var token = headers.GetValues("token");
            //如果没有token,或者有token无值
            if (token == null || !token.Any()) {
                throw new Exception("请登录");
            }
            //有值就获取第一个，然后解密
            return DEcode(token.First());
        }
    }
}