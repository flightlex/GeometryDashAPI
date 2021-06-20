﻿using System.Collections.Generic;
using GeometryDashAPI.Server.Dtos;

namespace GeometryDashAPI.Server.Responses
{
    public class AccountCommentPageResponse : GameStruct, IServerResponseCode
    {
        [StructPosition(0)]
        [ArraySeparator("|")]
        public List<AccountCommentDto> Comments { get; set; }
        
        [StructPosition(1)]
        public Pagination Page { get; set; }
        
        public override string GetParserSense() => "#";
        
        public int ResponseCode { get; set; }
    }
}