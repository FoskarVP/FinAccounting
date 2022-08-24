using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinAccountingWebService.APIReciept
{
    public enum ResponseCodeEnum
    {
        UncorrectReceipt = 0, // чек некорректен
        Success = 1, // данные чека получены (успешный запрос)
        NotReceivedYet = 2, // данные чека пока не получены
        TooManyRequest = 3, // превышено кол-во запросов
        WaitBeforeRetry = 4, // ожидание перед повторным запросом
        Other = 5 // прочее (данные не получены)
    }
}
