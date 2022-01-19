import { Injectable } from '@angular/core';
import { Location } from '@angular/common';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable, BehaviorSubject, throwError } from 'rxjs';

import { ConfigService } from './config.service';
import { ODataClient } from './odata-client';
import * as models from './radnet.model';

@Injectable()
export class RadnetService {
  odata: ODataClient;
  basePath: string;

  constructor(private http: HttpClient, private config: ConfigService) {
    this.basePath = config.get('radnet');
    this.odata = new ODataClient(this.http, this.basePath, { legacy: false, withCredentials: true });
  }

  getDboLeituras(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) : Observable<any> {
    return this.odata.get(`/DboLeituras`, { filter, top, skip, orderby, count, expand, format, select });
  }

  getEstacaos(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) : Observable<any> {
    return this.odata.get(`/Estacaos`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createEstacao(expand: string | null, estacao: models.Estacao | null) : Observable<any> {
    return this.odata.post(`/Estacaos`, estacao, { expand }, []);
  }

  deleteEstacao(idEstacao: number | null) : Observable<any> {
    return this.odata.delete(`/Estacaos(${idEstacao})`, item => !(item.id_estacao == idEstacao));
  }

  getEstacaoByidEstacao(expand: string | null, idEstacao: number | null) : Observable<any> {
    return this.odata.getById(`/Estacaos(${idEstacao})`, { expand });
  }

  updateEstacao(expand: string | null, idEstacao: number | null, estacao: models.Estacao | null) : Observable<any> {
    return this.odata.patch(`/Estacaos(${idEstacao})`, estacao, item => item.id_estacao == idEstacao, { expand }, []);
  }

  getGrauSensibilidades(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) : Observable<any> {
    return this.odata.get(`/GrauSensibilidades`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createGrauSensibilidade(expand: string | null, grauSensibilidade: models.GrauSensibilidade | null) : Observable<any> {
    return this.odata.post(`/GrauSensibilidades`, grauSensibilidade, { expand }, []);
  }

  deleteGrauSensibilidade(idGrau: number | null) : Observable<any> {
    return this.odata.delete(`/GrauSensibilidades(${idGrau})`, item => !(item.id_Grau == idGrau));
  }

  getGrauSensibilidadeByidGrau(expand: string | null, idGrau: number | null) : Observable<any> {
    return this.odata.getById(`/GrauSensibilidades(${idGrau})`, { expand });
  }

  updateGrauSensibilidade(expand: string | null, idGrau: number | null, grauSensibilidade: models.GrauSensibilidade | null) : Observable<any> {
    return this.odata.patch(`/GrauSensibilidades(${idGrau})`, grauSensibilidade, item => item.id_Grau == idGrau, { expand }, []);
  }

  getLeituras(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) : Observable<any> {
    return this.odata.get(`/Leituras`, { filter, top, skip, orderby, count, expand, format, select });
  }

  getNivelRadiacaos(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) : Observable<any> {
    return this.odata.get(`/NivelRadiacaos`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createNivelRadiacao(expand: string | null, nivelRadiacao: models.NivelRadiacao | null) : Observable<any> {
    return this.odata.post(`/NivelRadiacaos`, nivelRadiacao, { expand }, ['Sensor', 'ValorReferencium']);
  }

  deleteNivelRadiacao(idNivel: number | null) : Observable<any> {
    return this.odata.delete(`/NivelRadiacaos(${idNivel})`, item => !(item.id_nivel == idNivel));
  }

  getNivelRadiacaoByidNivel(expand: string | null, idNivel: number | null) : Observable<any> {
    return this.odata.getById(`/NivelRadiacaos(${idNivel})`, { expand });
  }

  updateNivelRadiacao(expand: string | null, idNivel: number | null, nivelRadiacao: models.NivelRadiacao | null) : Observable<any> {
    return this.odata.patch(`/NivelRadiacaos(${idNivel})`, nivelRadiacao, item => item.id_nivel == idNivel, { expand }, ['Sensor','ValorReferencium']);
  }

  getSensors(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) : Observable<any> {
    return this.odata.get(`/Sensors`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createSensor(expand: string | null, sensor: models.Sensor | null) : Observable<any> {
    return this.odata.post(`/Sensors`, sensor, { expand }, ['Estacao', 'GrauSensibilidade']);
  }

  deleteSensor(idSensor: number | null) : Observable<any> {
    return this.odata.delete(`/Sensors(${idSensor})`, item => !(item.id_sensor == idSensor));
  }

  getSensorByidSensor(expand: string | null, idSensor: number | null) : Observable<any> {
    return this.odata.getById(`/Sensors(${idSensor})`, { expand });
  }

  updateSensor(expand: string | null, idSensor: number | null, sensor: models.Sensor | null) : Observable<any> {
    return this.odata.patch(`/Sensors(${idSensor})`, sensor, item => item.id_sensor == idSensor, { expand }, ['Estacao','GrauSensibilidade']);
  }

  getValorReferencia(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) : Observable<any> {
    return this.odata.get(`/ValorReferencia`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createValorReferencium(expand: string | null, valorReferencium: models.ValorReferencium | null) : Observable<any> {
    return this.odata.post(`/ValorReferencia`, valorReferencium, { expand }, []);
  }

  deleteValorReferencium(idReferencia: number | null) : Observable<any> {
    return this.odata.delete(`/ValorReferencia(${idReferencia})`, item => !(item.id_referencia == idReferencia));
  }

  getValorReferenciumByidReferencia(expand: string | null, idReferencia: number | null) : Observable<any> {
    return this.odata.getById(`/ValorReferencia(${idReferencia})`, { expand });
  }

  updateValorReferencium(expand: string | null, idReferencia: number | null, valorReferencium: models.ValorReferencium | null) : Observable<any> {
    return this.odata.patch(`/ValorReferencia(${idReferencia})`, valorReferencium, item => item.id_referencia == idReferencia, { expand }, []);
  }
}
