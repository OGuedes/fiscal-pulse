export type JourneyStep = { title: string; detail: string; done: boolean };
export type ProductConfig = { name:string; eyebrow:string; title:string; subtitle:string; accent:string; accentSoft:string; metric:string; metricLabel:string; steps:JourneyStep[]; cardTitle:string; cardDetail:string; badge:string; action:string; tabs:string[] };
export const product: ProductConfig = {
  name:'Fiscal Pulse', eyebrow:'ATELIÊ AURORA LTDA.', title:'Obrigações em dia, decisões à frente.', subtitle:'Visão contábil de outubro', accent:'#2563EB', accentSoft:'#DFEAFE', metric:'2', metricLabel:'pendências neste ciclo',
  steps:[{title:'Notas fiscais',detail:'Conferência concluída',done:true},{title:'Folha de pagamento',detail:'Valores validados',done:true},{title:'Guia de impostos',detail:'Vence em 3 dias',done:false}],
  cardTitle:'Documento solicitado',cardDetail:'Envie o extrato bancário de setembro',badge:'PENDENTE',action:'Anexar documento',tabs:['Resumo','Agenda','Documentos','Mensagens']
};
