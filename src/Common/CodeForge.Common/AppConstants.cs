namespace CodeForge.Common;

public class AppConstants
{
    public const string RABBITMQ_HOST = "localhost";
    public const string DEFAULT_EXCHANGE_TYPE = "direct";

    #region EXCHANGES
    public const string USER_EXCHANGE_NAME = "user_exchange";
    public const string FAV_EXCHANGE_NAME = "fav_exchange";
    public const string VOTE_EXCHANGE_NAME = "vote_exchange";

    #endregion

    #region QUEUES
    public const string USER_EMAIL_CHANGED_QUEUE_NAME = "user_email_changed_queue";
    public const string CREATE_COMMENT_FAV_QUEUE_NAME = "create_entry_comment_fav";
    public const string CREATE_ENTRY_VOTE_QUEUE_NAME = "create_entry_vote";
    public const string DELETE_ENTRY_VOTE_QUEUE_NAME = "delete_entry_vote";
    public const string CREATE_ENTRY_FAV_QUEUE_NAME = "create_entry_fav";
    public const string DELETE_ENTRY_FAV_QUEUE_NAME = "delete_entry_fav";

    #endregion


}
