export interface DboLeitura {
  hora: string;
  Abrantes: string;
  Beja: string;
  Bragana: string;
  Castelo_Branco: string;
  Coimbra: string;
  Elvas: string;
  vora: string;
  Faro: string;
  Funchal: string;
  Junqueira: string;
  Lisboa: string;
  Meimoa: string;
  Penhas_Douradas: string;
  Pocinho: string;
  Ponta_Delgada: string;
  Portalegre: string;
  Porto: string;
  Sines: string;
}

export interface Estacao {
  id_estacao: number;
  tipo_estacao: string;
  freq_leitura: string;
  localizacao: string;
  data_instalacao: string;
}

export interface GrauSensibilidade {
  id_Grau: number;
  limite_maximo: number;
  limite_minimo: number;
}

export interface Leitura {
  id_leitura: number;
  data_leitura: string;
  hora_leitura: string;
  valor_leitura: number;
  id_estacao: number;
}

export interface NivelRadiacao {
  id_nivel: number;
  freq_radiacao: number;
  id_sensor: number;
  id_referencia: number;
}

export interface Sensor {
  tipo_sensor: string;
  id_sensor: number;
  id_estacao: number;
  id_Grau: number;
}

export interface ValorReferencium {
  id_referencia: number;
  VR_diario: number;
  VR_mensal: number;
  VR_anual: number;
}
