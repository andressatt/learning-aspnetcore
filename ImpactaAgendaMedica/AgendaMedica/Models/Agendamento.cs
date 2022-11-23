using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;

namespace AgendaMedica.Models
{
    public class Agendamento
    {
        private DateTimeOffset? _horarioFinalAtendimento;

        public Agendamento()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Id do item na agenda.
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        /// <summary>
        /// Horário do agendamento.
        /// </summary>
        [DisplayName("Horário de agendamento")]
        public DateTimeOffset HorarioAgendamento { get; set; }

        /// <summary>
        /// Tempo da consulta.
        /// </summary>
        [DisplayName("Tempo da consulta")]
        public TimeSpan TempoConsulta { get; set; }

        /// <summary>
        /// Horário final do atendimento previsto.
        /// </summary>
        [DisplayName("Horário Final da Consulta")]
        public DateTimeOffset HorarioFinalAtendimento { 
            get { 
                if (_horarioFinalAtendimento == null) 
                    SetHorarioFinalAtendimento();
                return _horarioFinalAtendimento!.Value; 
            }
            set => SetHorarioFinalAtendimento();
        }

        private void SetHorarioFinalAtendimento()
        {
            var novoHorario = HorarioAgendamento.Add(TempoConsulta);
            if (_horarioFinalAtendimento == null || _horarioFinalAtendimento != novoHorario)
                _horarioFinalAtendimento = novoHorario;
        }
    }
}
