namespace Modsen.Server.CarsElections.Domain.Constants
{
    public static class ErrorConstants
    {
        public const string NotFoundAccessTokenError = "NotFoundAccessToken";
        public const string NotFoundRefreshTokenError = "NotFoundRefreshToken";
        public const string NotFoundUserError = "NotFoundUser";
        public const string LoginError = "CheckLoginAndPassword";
        public const string ServerSideError = "ServerSideError";
        public const string InvalidRefreshTokenError = "InvalidRefreshToken";
        public const string CarNotFoundError = "CarNotFound";
        public const string LikeNotFoundError = "LikeNotFound";
        public const string CommentNotFoundError = "CommentNotFound";
        public const string CommentAlreadyExistsError = "CommentAlreadyExists";
        public const string LikeAlreadyExistsError = "LikeAlreadyExists";
    }
}
