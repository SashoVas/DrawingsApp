export interface IGroup{
    id:number,
    title:string,
    moreInfo:string,
    imgUrl:string,
    groupType:number,
    tags:Array<string>,
    users:number,
    admins:number,
    role:number,
    isJoined:boolean,
    notifications:boolean
}