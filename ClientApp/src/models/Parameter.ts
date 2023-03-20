import { Utilities } from "../helpers/Utilities";

export class Parameter {
    name: string;
    value: string | null;
    private defaultValue: string | null = null;

    constructor(name: string, value: any) {
        this.name = name;

        this.value = this.LoadValue(value);
    }

    //Carregar valor
    private LoadValue(value: any): string | null {

        //Verificamos se o valor está definido, caso contrário colocamos lá null
        var newValue = Utilities.IfIsUndefinedOrNullThen(value, this.defaultValue);

        //Se for uma string e estiver com caracteres não visiveis enviamos o valor default
        if (typeof newValue == 'string' && newValue.trim().length <= 0) return this.defaultValue;

        if (typeof newValue == "number") return newValue.toString();

        //Caso contrário mandamos o novo valor
        return newValue;
    }
}