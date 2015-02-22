function rateConfession(postId) {
    var cookie = readCookie("confessionRating" + postId);
    if (cookie == null) {
        alert("cookie is null, creating");
        // cookie is not set - set it, then bump this post's rating
        createCookie("confessionRating" + postId, true, 2000);
    }
    else if (cookie) {
        alert("cookie is not null, deleting");
        // this has already been set, so delete it and then 
        // downgrade this post's rating
        eraseCookie("confessionRating" + postId);
    }
}

function didRateConfession(postId) {
    var cookie = readCookie("confessionRating" + postId);
    if (cookie == null) {
        return false;
    }
    else
        return true;
}

