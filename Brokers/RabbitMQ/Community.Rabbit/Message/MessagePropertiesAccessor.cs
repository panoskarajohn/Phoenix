using Community.Rabbit.Abstractions.Message;

namespace Community.Rabbit.Message;

public class MessagePropertiesAccessor : IMessagePropertiesAccessor
{
    private static readonly AsyncLocal<MessageContextHolder>
        Holder = new();

    public IMessageProperties MessageProperties
    {
        get => Holder.Value?.Properties;
        set
        {
            var holder = Holder.Value;
            if (holder != null)
            {
                holder.Properties = null;
            }

            if (value != null)
            {
                Holder.Value = new MessageContextHolder {Properties = value};
            }
        }
    }

    private class MessageContextHolder
    {
        public IMessageProperties Properties;
    }
}