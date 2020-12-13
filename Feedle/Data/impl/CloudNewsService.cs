﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Feedle.Models;
using Microsoft.AspNetCore.Mvc;

namespace Feedle.Data
{
    public class CloudNewsService : INewsService
    {
        public List<Post> CurrentPosts { get; set; }

        public HttpClient Client { get; set; }

        public CloudNewsService()
        {
            Client = new HttpClient();
        }
        public async Task<bool> AddPostAsync(Post post)
        {
            string postToSerialize = JsonSerializer.Serialize(post);
            Console.WriteLine(postToSerialize);
            StringContent stringContent = new StringContent(
                postToSerialize,
                Encoding.UTF8,
                "application/json"
            );
            HttpResponseMessage responseMessage =
                await Client.PostAsync("http://localhost:5002/feedle/posts", stringContent);
            return responseMessage.IsSuccessStatusCode;
        }

        public async Task<IList<Post>> GetAllNews()
        {
            String message = await Client.GetStringAsync("http://localhost:5002/feedle/posts");
            if (JsonSerializer.Deserialize<List<Post>>(message) == null)
            {
                return new List<Post>();
            }
            return JsonSerializer.Deserialize<List<Post>>(message);
        }

        public async Task UpdatePostAsync(Post post)
        {
            string postToSerialize = JsonSerializer.Serialize(post);
            Console.WriteLine(postToSerialize);
            StringContent stringContent = new StringContent(
                postToSerialize,
                Encoding.UTF8,
                "application/json");
            HttpResponseMessage httpResponseMessage=
                await Client.PatchAsync("http://localhost:5002/feedle/posts", stringContent);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
               
            }
        }

        public async Task<List<Post>> GetPostsForRegisteredUser(int id)
        {
            String message = await Client.GetStringAsync("http://localhost:5002/feedle/posts/authorized?id="+id);
            if (message.Length == 0)
            {
                return new List<Post>();
            }
            return JsonSerializer.Deserialize<List<Post>>(message);
        }

        public async Task<bool> CommentPost(Comment comment, int postId)
        {
            String commentAsJson = JsonSerializer.Serialize(comment);
            StringContent stringContent = new StringContent(
                commentAsJson,
                Encoding.UTF8,
                "application/json"
            );
            HttpResponseMessage responseMessage =
                await Client.PostAsync("http://localhost:5002/feedle/posts/comment?Id=" + postId, stringContent);
            return responseMessage.IsSuccessStatusCode;
        }

        public Task<bool> DeletePost(Post post)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteComment(Post post, int commentId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsPostThumbUpByUser(Post post, User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsPostThumbDownByUser(Post post, User user)
        {
            throw new NotImplementedException();
        }
    }
}