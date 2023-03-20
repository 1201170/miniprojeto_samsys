import { PageURL } from "./PageURL";

class UtilitiesClass{

        //Método para verificar se valor é inteiro
        public IsInteger(value: any): boolean {

            //Significa que tem caracteres de uma string, logo é falso
            if (/^(\-|\+)?([0-9]+)$/.test(value) === false) return false;

            var valueInNumber = Number(value);
            return Number.isInteger(valueInNumber) && (Math.abs(valueInNumber) < 2147483647);
        }

        //Método para verificar se valor é data
        public IsDate(value: any): boolean {
            switch (typeof value) {
                case 'number':
                    return true;
                case 'string':
                    return !isNaN(Date.parse(value));
                case 'object':
                    if (value instanceof Date) {
                        return !isNaN(value.getTime());
                    } else {
                        return false;
                    }
                default:
                    return false;
            }
        }

        //Método para adicionar um query parameter ao URL
        public AddParamToQueryIfValue(queryParamName: string, type: "string" | "number" | "date", value: any): string {
            if (typeof value == 'undefined' || value == null) return '';
    
            //Se o tipo do parâmetro que queremos for string
            if (type === "string") {
                //Tentamos ver se a string tem alguma coisa
                if (value.trim() === "") return "";
            }
    
            if (type === "date") {
                if (this.IsDate(value) === false) return "";
            }
    
            if (type === "number") {
                if (this.IsInteger(value) === false) return "";
            }
    
            return `&${queryParamName}=${value}`;
        }
    
        //Método para obter um parâmetro dos parâmetros do URL convertido já como o valor correto    
        public LoadParameterFromURLQuery(parameterName: string, validateParameterType: "string" | "date" | "number", defaultValue: any): any {
            try {
                var searchParams = new URLSearchParams(PageURL.GetSearchParameters());
    
                var parameterExist = searchParams.get(parameterName);
                if (parameterExist == null) return defaultValue;
    
                switch (validateParameterType) {
                    case "date":
    
                        //É uma data
                        if (this.IsDate(parameterExist) === true) {
                            return parameterExist;
                        } else {
                            return defaultValue;
                        }
    
                    case "number":
                        if (this.IsInteger(parameterExist) === true) {
                            return parseInt(parameterExist);
                        } else {
                            return defaultValue;
                        }
    
                    case "string":
                        if (typeof parameterExist == 'undefined' || parameterExist == null || parameterExist.trim().length <= 0) return defaultValue;
                        return parameterExist.toString();
                }
    
            } catch (exp) {
                return defaultValue;
            }
        }    

        //Método para retonar valor default se undefined ou a null
        public IfIsUndefinedOrNullThen(value: any, defaultValue: any) {
            if (typeof value == 'undefined' || value == null) return defaultValue;
            return value;
        }

}

export const Utilities = new UtilitiesClass();