import { IComment } from "./IComment";

export interface IPostFull{
    outerId:number,
    imgUrls:Array<string>,
    description:string,
    postedOn:string,
    title:string,
    likes:number,
    senderName:string,
    groupName:string,
    groupId:number,
    comments:Array<IComment>
}