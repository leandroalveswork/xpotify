import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DataHelper } from 'helper/datahelper';
import { Router } from '@angular/router';

@Component({
    selector: 'app-audio',
    templateUrl: './audio.component.html'
})
export class AudioComponent {
    private _http: HttpClient;
    private _router: Router;
    private _baseUrl: string;
    public audiosListados: AudioListadoRetorno[] = [];
    public audiosMostrados: AudioMostrado[] = [];
    public filtroDeArquivoNome: string = '';


    constructor(http: HttpClient, router: Router, @Inject('BASE_URL') baseUrl: string) {
        this._http = http;
        this._router = router;
        this._baseUrl = baseUrl;
        http.get<AudioListadoRetorno[]>(baseUrl + 'api/audio/listar').subscribe(result => {
            this.atualizarAudios(result);
        });
    }

    atualizarAudios(audiosListados: AudioListadoRetorno[]) {
        this.audiosListados = audiosListados;
        this.audiosMostrados = audiosListados.map(iAudio => {
            var novoAudioMostrado = new AudioMostrado(this._http, this._router, this._baseUrl);
            novoAudioMostrado.id = iAudio.id;
            novoAudioMostrado.arquivoNome = iAudio.arquivoNome;
            novoAudioMostrado.tipo = iAudio.tipoEEntrada ? "Entrada" : "Saída";
            novoAudioMostrado.operadorNome = iAudio.operadorNome;
            novoAudioMostrado.dataCriacao = DataHelper.transformarDataDeApiParaDataMostrada(iAudio.dataCriacao);
            return novoAudioMostrado;
        });
    }

    filtrar() {
        if (this.filtroDeArquivoNome == '') {
            this._http.get<AudioListadoRetorno[]>(this._baseUrl + 'api/audio/listar').subscribe(result => {
                this.atualizarAudios(result);
            });
        } else {
            this._http.get<AudioListadoRetorno[]>(this._baseUrl + 'api/audio/listarPorArquivoNome?arquivoNome=' + this.filtroDeArquivoNome).subscribe(result => {
                this.atualizarAudios(result);
            });
        }
    }

    navegarParaIncluir() {
        this._router.navigateByUrl('/audio/incluiraudio');
    }

    navegarParaAlterar(id: number) {
        this._router.navigateByUrl('/audio/alteraraudio?id=' + id);
    }
}

interface AudioListadoRetorno {
    id: number;
    arquivoNome: string;
    tipoEEntrada: boolean;
    operadorNome: string;
    dataCriacao: string;
}

class AudioMostrado {
    private _http: HttpClient;
    private _router: Router;
    private _baseUrl: string;

    constructor(http: HttpClient, router: Router, baseUrl: string) {
        this._http = http;
        this._router = router;
        this._baseUrl = baseUrl;
    }
    id!: number
    arquivoNome!: string;
    tipo!: string;
    operadorNome!: string;
    dataCriacao!: string;
    navegarParaAlterar() {
        this._router.navigateByUrl('/audio/alteraraudio?id=' + this.id);
    }
    excluir() {
        this._http.get(this._baseUrl + 'api/audio/excluir?id=' + this.id).subscribe(result => {
            window.location.reload();
        });
    }
}
