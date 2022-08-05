import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
    selector: 'app-incluiraudio',
    templateUrl: './incluiraudio.component.html'
})
export class IncluirAudioComponent {
    private _http: HttpClient;
    private _router: Router;
    private _baseUrl: string;
    public arquivoNome: string = '';
    public tipoEEntrada: string = '';
    public operadorNome: string = '';
    public mensagemErro: string = '';

    constructor(http: HttpClient, router: Router, @Inject('BASE_URL') baseUrl: string) {
        this._http = http;
        this._router = router;
        this._baseUrl = baseUrl;
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
        let novoAudio = {
            arquivoNome: this.arquivoNome.trim(),
            tipoEEntrada: this.tipoEEntrada == "S",
            operadorNome: this.operadorNome.trim()
        };
        this._http.post(this._baseUrl + 'api/audio/incluir', novoAudio).subscribe(result => {
            this._router.navigateByUrl('/audio');
        });
    }
}