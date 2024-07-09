using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTradeSuite.Common.GlobalConstants
{
    public class HttpConstants
    {
        public const string ENTITTIES_NOT_FOUND_RESULT = "No {0} were found";

        public const string GET_ALL_ENTITIES_EXCEPTION_MESSAGE = "Something went wrong when retrieving the {0} \n {1}";

        public const string GET_ALL_ENTITIES_WITH_DELETED_RECORDS_EXCEPTION_MESSAGE = "Something went wrong, " +
            "when trying to retrieve all the {0}, including the deleted records \n {1}";

        public const string INTERNAL_SERVER_ERROR_MESSAGE = "Internal Server Error";

        public const string ENTITY_BY_ID_NOT_FOUND_RESULT = "The {0} with id {1} couldn't be found";

        public const string GET_ENTITY_BY_ID_EXCEPTION_MESSAGE = "Something went wrong when retrieving the " +
            "{0} with an id {1} \n {2}";

        public const string GET_ENTITY_DETAILS_EXCEPTION_MESSAGE = "Something went wrong when retrieving the " +
            "{0} details with an id {1} \n {2}";

        public const string INVALID_OBJECT_FOR_ENTITY_CREATION = "The {0} object, sent from the client is null";

        public const string BAD_REQUEST_MESSAGE = "The {0} {1} object is null";

        public const string INVALID_OBJECT_FOR_ENTITY_UPDATE = "The {0} object for update, sent from the client, is null";

        public const string INVALID_OBJECT_FOR_ENTITY_PATCH = "The {0} object for patch, sent from the client, is null";

        public const string ENTITY_CREATION_EXCEPTION_MESSAGE = "Something went wrong when trying to create a {0} \n {1}";

        public const string ENTITY_UPDATE_EXCEPTION_MESSAGE = "Something went wrong when trying to update a {0} \n {1}";

        public const string ENTITY_DELETION_EXCEPTION_MESSAGE = "Something went wrong when trying to delete the " +
            "{0} with provided id {1} {2}";

        public const string ENTITY_HARD_DELETION_EXCEPTION_MESSAGE = "Something went wrong when trying to delete the " +
            "{0} completely with provided id {1} {2}";

        public const string ENTITY_RESTORE_EXCEPTOIN_MESSAGE = "Something went wrong when trying to restore the " +
            "{0} with provided id {1} {2}";

        public const string CREATE_ACTION = "creation";

        public const string UPDATE_ACTION = "update";

        public const string FAILED_UPDATE = "Failed to update {0}: ";

        public const string FAILED_DELETE = "Failed to delete {0}: ";

        public const string FAILED_DELETE_CONFIRMATION = "Failed to confirm deletion for {0}: ";
    }
}
