--create database Radnet;

use radnetBD;


CREATE TABLE Estacao
(
  id_estacao INT NOT NULL IDENTITY,
  tipo_estacao VARCHAR(30) NOT NULL,
  freq_leitura VARCHAR(20) NOT NULL,
  localizacao VARCHAR(50) NOT NULL,
  data_instalacao DATE NOT NULL,
  PRIMARY KEY (id_estacao)
);

CREATE TABLE Grau__sensibilidade
(
  id_Grau INT NOT NULL IDENTITY,
  limite_maximo FLOAT NOT NULL ,
  limite_minimo FLOAT NOT NULL,
  PRIMARY KEY (id_Grau)
);

CREATE TABLE valor_referencia
(
  id_referencia INT NOT NULL IDENTITY,
  VR_diario FLOAT NOT NULL,
  VR_mensal FLOAT NOT NULL,
  VR_anual FLOAT NOT NULL,
  PRIMARY KEY (id_referencia)
);

CREATE TABLE Sensor
(
  tipo_sensor VARCHAR(10) NOT NULL,
  id_sensor INT NOT NULL IDENTITY,
  id_estacao INT,
  id_Grau INT NOT NULL,
  PRIMARY KEY (id_sensor),
  FOREIGN KEY (id_estacao) REFERENCES Estacao(id_estacao),
  FOREIGN KEY (id_Grau) REFERENCES Grau__sensibilidade(id_Grau)
);

CREATE TABLE nivel_radiacao
(
  id_nivel INT NOT NULL IDENTITY,
  freq_radiacao INT NOT NULL,
  id_sensor INT NOT NULL,
  id_referencia INT NOT NULL,
  PRIMARY KEY (id_nivel),
  FOREIGN KEY (id_sensor) REFERENCES Sensor(id_sensor),
  FOREIGN KEY (id_referencia) REFERENCES valor_referencia(id_referencia)
);


CREATE TABLE leituras
(
	id_leitura INT NOT NULL IDENTITY,
	data_leitura DATE,
	hora_leitura TIME,
	valor_leitura FLOAT,
	id_estacao INT NOT NULL

	FOREIGN KEY (id_estacao) REFERENCES Estacao(id_estacao)
);

