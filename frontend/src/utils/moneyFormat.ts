export function moneyFormat(number : number | undefined){
    if(!number){
        return "";
    }
    return number.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ");
}