using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Automatonymous;

namespace GDStore.DAL.Interface.Domain
{
    [Table("AlterationSagas")]
    public class AlterationSaga : SagaStateMachineInstance
    {
        [Key]
        public Guid CorrelationId { get; set; }
        public string CurrentState { get;set; }
        public Guid AlterationId { get; set; }
        public DateTime Created { get; set; }
    }
}