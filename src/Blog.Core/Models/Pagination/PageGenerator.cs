﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Core.Models.Contexts;
using Blog.Core.Models.Interfaces;
using Blog.Core.Models.Settings;
using Blog.Core.Models.Templating.Interfaces;
using Microsoft.Extensions.Options;

namespace Blog.Core.Models.Pagination
{
    public class PageGenerator: IPageGenerator
    {
        private readonly IBlogContext _blogContext;
        private readonly IPostFacade _facade;
        private readonly int _postsPerPage;

        
        public PageGenerator(IBlogContext blogContext, IPostFacade facade, IOptions<SiteSettings> siteSettings)
        {
            _blogContext = blogContext;
            _facade = facade;
            _postsPerPage = siteSettings.Value.PostsPerPage;
        }

        public async Task<IPageContext> GetContextForPage(int pageNum)
        {
            return await new PageContext(_blogContext, _facade, pageNum, _postsPerPage).InitializeAsync();
        }

        public async Task<IPageContext> GetFilteredPostPageContext(Func<IEnumerable<Post>, IEnumerable<Post>> filter)
        {
            return await new PageContext(_blogContext, _facade, filter).InitializeAsync();
        }

        public IPageContext GetMetadataOnlyContext()
        {
            return new PageContext(_blogContext);
        }
    }
}