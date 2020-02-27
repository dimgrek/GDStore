using System;
using System.Threading.Tasks;
using Automatonymous;
using GDStore.Alterations.Messages.Commands;
using GDStore.Alterations.Messages.Events;
using GDStore.DAL.Interface.Domain;
using log4net;

namespace GDStore.Alterations.Sagas
{
    public class AlterationStateMachine : MassTransitStateMachine<AlterationSaga>
    {
        private readonly ILog log = LogManager.GetLogger(typeof(AlterationStateMachine));

        public AlterationStateMachine()
        {
            InstanceState(x => x.CurrentStatus.ToString());

            Event(() => AlterationAdded, x => x.CorrelateById(ctx => ctx.Message.Id));
            Event(() => AlterationFinished, x => x.CorrelateById(ctx => ctx.Message.Id));
            Event(() => PaymentDone, x => x.CorrelateById(ctx => ctx.Message.Id));

            Initially(
                When(AlterationAdded)
                    .Then(ctx => log.Info($"{nameof(AlterationStateMachine)}: {nameof(AlterationAddedEvent)} handled"))
                    .Then(SetInitialState)
                    .TransitionTo(Active)
            );

            Initially(
                When(PaymentDone)
                    .Then(ctx => log.Info($"{nameof(AlterationStateMachine)}: {nameof(PaymentDoneEvent)} handled"))
                    .Publish(( ctx) => new MakeAlterationCommand
                    {
                        //AlterationId = ctx.Data.AlterationId
                    })
                    .TransitionTo(Active));


            //.ThenAsync(ctx => HandlePayment(ctx))

        }

        private async Task HandlePayment(BehaviorContext<AlterationSaga, PaymentDoneEvent> ctx)
        {
            var x = ctx.Data.AlterationId;
            //new MakeAlterationCommand
            //{
            //    AlterationId = ctx.Data.AlterationId
            //}
        }

        private static void SetInitialState(BehaviorContext<AlterationSaga, AlterationAddedEvent> ctx)
        {
            ctx.Instance.AlterationId = ctx.Data.AlterationId;
            ctx.Instance.Created = DateTime.Now;
        }

        public State Active { get; private set; }

        public Event<AlterationAddedEvent> AlterationAdded { get; private set; }
        public Event<AlterationFinishedEvent> AlterationFinished { get; private set; }
        public Event<PaymentDoneEvent> PaymentDone { get; private set; }
    }
}
