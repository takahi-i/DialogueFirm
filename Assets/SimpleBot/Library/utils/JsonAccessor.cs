/**
Copyright (c) 2017 Koki Ibukuro

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// simple access to json
///
/// var json = JsonNode.Parse(jsonString);
/// string foo = json["hoge"][4].Get<string>();
/// </summary>
/// 
namespace SimpleBot
{
    namespace Utils
    {
        public class JsonAccessor : IEnumerable<JsonAccessor>, IDisposable
        {
            object currentNode;

            public JsonAccessor(object obj)
            {
                this.currentNode = obj;
            }

            public void Dispose()
            {
                currentNode = null;
            }

            public static JsonAccessor Parse(string json)
            {
                return new JsonAccessor(MiniJSON.Json.Deserialize(json));
            }

            public JsonAccessor this[int i]
            {
                get
                {
                    if (currentNode is IList)
                    {
                        return new JsonAccessor(((IList)currentNode)[i]);
                    }
                    throw new Exception("Node is not List : " + currentNode.GetType().ToString());
                }
            }

            public JsonAccessor this[string key]
            {
                get
                {
                    if (currentNode is IDictionary)
                    {
                        return new JsonAccessor(((IDictionary)currentNode)[key]);
                    }
                    throw new Exception("Node is not Dictionary : " + currentNode.GetType().ToString());
                }
            }

            public int Count
            {
                get
                {
                    if (currentNode is IList)
                    {
                        return ((IList)currentNode).Count;
                    }
                    else if (currentNode is IDictionary)
                    {
                        return ((IDictionary)currentNode).Count;
                    }
                    else
                    {
                        return 0;
                    }
                }

            }

            public bool Contains<T>(T key)
            {
                if (currentNode is IDictionary)
                {
                    var dic = (IDictionary)currentNode;
                    return dic.Contains(key);
                }
                else
                {
                    return false;
                }
            }

            public T Get<T>()
            {
                return (T)currentNode;
            }

            public IEnumerator<JsonAccessor> GetEnumerator()
            {
                if (currentNode is IList)
                {
                    foreach (var o in (IList)currentNode)
                    {
                        yield return new JsonAccessor(o);
                    }
                }
                else if (currentNode is IDictionary)
                {
                    var dic = (IDictionary)currentNode;
                    foreach (var o in dic.Keys) // return keys
                    {
                        yield return new JsonAccessor(o);
                    }
                }
                else
                {
                    yield return null;
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}