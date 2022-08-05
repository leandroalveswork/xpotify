import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router, Params } from '@angular/router';

@Component({
    selector: 'app-alteraraudio',
    templateUrl: './alteraraudio.component.html'
})
export class AlterarAudioComponent {
    private _http: HttpClient;
    private _router: Router;
    private _baseUrl: string;
    private _activatedRoute: ActivatedRoute;
    public idFoiCarregado: boolean = false;
    public id: number = 0;
    public arquivoNome: string = '';
    public tipoEEntrada: string = '';
    public operadorNome: string = '';
    public mensagemErro: string = '';

    constructor(http: HttpClient, router: Router, activatedRoute: ActivatedRoute, @Inject('BASE_URL') baseUrl: string) {
        this._http = http;
        this._router = router;
        this._activatedRoute = activatedRoute;
        this._baseUrl = baseUrl;
        activatedRoute.queryParams.subscribe(params => {
            this.idFoiCarregado = true;
            this.id = params['id'];
            console.log(this.id);
            this._http.get<AudioPorIdRetorno>(baseUrl + 'api/audio/obterPorId?id=' + this.id).subscribe(result => {
                this.arquivoNome = result.arquivoNome;
                this.tipoEEntrada = result.tipoEEntrada ? 'S' : 'N';
                this.operadorNome = result.operadorNome;
                this.mensagemErro = '';
            });
        });
    }

    salvar() {
        if (this.arquivoNome.trim() == '') {
            this.mensagemErro = 'É obrigatório informar o Nome do Arquivo';
            return;
        }
        if (this.tipoEEntrada == '') {
            this.mensagemErro = 'É obrigatório selecionar o Tipo';
            return;
        }
        if (this.operadorNome == '') {
            this.mensagemErro = 'É obrigatório informar o Nome do Operador';
            return;
        }
        let alterarAudio = {
            id: this.id,
            arquivoNome: this.arquivoNome.trim(),
            tipoEEntrada: this.tipoEEntrada == "S",
            operadorNome: this.operadorNome.trim()
        };
        this._http.post(this._baseUrl + 'api/audio/alterar', alterarAudio).subscribe(result => {
            this._router.navigateByUrl('/audio');
        });
    }
}

interface AudioPorIdRetorno {
    id: number;
    arquivoNome: string;
    tipoEEntrada: boolean;
    operadorNome: string;
    dataCriacao: string;
}