namespace MarkNet.Core.Models.Cashing
{
    public class GetCashingResponse<T>
    {
        public bool IsSuccess { get; set; }
        public T Model { get; set; } = default!;
    }
}
