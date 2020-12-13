﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Feedle.Models;

namespace Feedle.Data
{
    public interface INewsService
    {
        Task<bool> AddPostAsync(Post post);
        Task<IList<Post>> GetAllNews();
        Task UpdatePostAsync(Post post);

        Task<List<Post>> GetPostsForRegisteredUser(int id);

        Task<bool> CommentPost(Comment comment, int postId);
        Task<bool> DeletePost(Post post);

        Task<bool> DeleteComment(Post post, int commentId);
        Task<bool> IsPostThumbUpByUser(Post post, User user);
        Task<bool> IsPostThumbDownByUser(Post post, User user);
    }
}