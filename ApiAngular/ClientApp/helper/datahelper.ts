export class DataHelper {
    public static transformarDataDeApiParaDataMostrada(dataDeApi: string): string {
        let ano = dataDeApi.substring(0, 4);
        let mes = dataDeApi.substring(5, 7);
        let dia = dataDeApi.substring(8, 10);
        let hora = dataDeApi.substring(11, 13);
        let minuto = dataDeApi.substring(14, 16);
        let segundo = dataDeApi.substring(17, 19);
        return `${dia}/${mes}/${ano} ${hora}:${minuto}:${segundo}`;
    }
}