using System;
using Automatonymous;
using GDStore.Alterations.Messages.Commands;
using GDStore.Alterations.Messages.Events;
using GDStore.DAL.Interface.Domain;
using GDStore.Notifications.Messages.Commands;
using GDStore.Payments.Messages.Commands;

namespace GDStore.Alterations.Sagas
{
    public class AlterationStateMachine : MassTransitStateMachine<AlterationSaga>
    {
        public AlterationStateMachine()
        {
            InstanceState(x => x.CurrentState);
            
            Event(() => AlterationAdded, x => x.CorrelateById(ctx => ctx.Message.Id));
            Event(() => MakePayment, 
                x => x.CorrelateById(state => state.AlterationId, ctx => ctx.Message.AlterationId));
            Event(() => AlterationFinished, 
                x => x.CorrelateById(state => state.AlterationId, ctx => ctx.Message.AlterationId));

            Initially(When(AlterationAdded)
                    .Then(SetInitialState)
                    .TransitionTo(Active));

            During(Active,
                When(MakePayment)
                    .Publish(ctx => new MakePaymentCommand {AlterationId = ctx.Data.AlterationId})
                    .Publish(ctx => new MakeAlterationCommand {AlterationId = ctx.Data.AlterationId})
                    .TransitionTo(Active));

            During(Active,
                When(AlterationFinished)
                    .Publish(ctx => new SendEmailCommand {AlterationId = ctx.Data.AlterationId})
                    .Finalize());
        }

        private static void SetInitialState(BehaviorContext<AlterationSaga, AlterationAddedEvent> ctx)
        {
            ctx.Instance.CorrelationId = Guid.NewGuid();
            ctx.Instance.AlterationId = ctx.Data.AlterationId;
            ctx.Instance.Created = DateTime.Now;
        }

        public State Active { get; private set; }
        public Event<AlterationAddedEvent> AlterationAdded { get; private set; }
        public Event<AlterationFinishedEvent> AlterationFinished { get; private set; }
        public Event<MakePaymentEvent> MakePayment { get; private set; }
    }
}
