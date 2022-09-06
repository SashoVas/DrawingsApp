import { ITag } from "./ITag";

export interface IGroup{
    id:number,
    title:string,
    moreInfo:string,
    imgUrl:string,
    groupType:number,
    tags:Array<ITag>,
    users:number,
    admins:number,
    role:number,
    isJoined:boolean,
    notifications:boolean
}