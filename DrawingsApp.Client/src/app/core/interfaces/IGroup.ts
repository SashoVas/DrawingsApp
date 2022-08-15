export interface IGroup{
    id:number,
    title:string,
    moreInfo:string,
    imgUrl:string,
    groupType:number,
    tags:Array<string>,
    users:number,
    role:number,
    isJoined:boolean
}