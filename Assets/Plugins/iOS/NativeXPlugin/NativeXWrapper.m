//
//  W3iWrapper.cpp
//  Unity-iPhone
//
//  Created by Josh Ruis on 1/18/13.
//
//

#import "NativeXCore.h"
#import "NativeXMonetizationSDK.h"

#define GetStringParam( _x_ ) ( _x_ != NULL ) ? [NSString stringWithUTF8String:_x_] : [NSString stringWithUTF8String:""]

#define GetStringParamOrNil( _x_ ) ( _x_ != NULL && strlen( _x_ ) ) ? [NSString stringWithUTF8String:_x_] : nil

void uStartWithNameAndApplicationId(const char *name, const char *appId, const char *pubId)
{
    [[NativeXCore instance] startWithName:GetStringParam(name) applicationId:GetStringParamOrNil(appId) publisherId:GetStringParamOrNil(pubId)];
    
}
void uShowOfferWall()
{
    [[NativeXCore instance] showOfferWall];
}

void uShowIncentOfferWall()
{
    [[NativeXCore instance] showIncentOfferWall];
}

void uShowNonIncentOfferWall()
{
    [[NativeXCore instance] showNonIncentOfferWall];
}

void uShowOfferWallFromPoint(float x, float y)
{
    [[NativeXCore instance] showOfferWallFromPoint:CGPointMake(x, y)];
}

void uGetAndCacheFeaturedOffer()
{
    [[NativeXCore instance]getAndCacheFeaturedOffer];
}

void uShowCachedFeaturedOffer()
{
    [[NativeXCore instance] showCachedFeaturedOffer];
}

void uShowFeaturedOffer()
{
    [[NativeXCore instance] showFeaturedOffer];
}

void uGetAndCacheInterstitial()
{
    [[NativeXCore instance] getAndCacheInterstitial];
}

void uShowCachedInterstitial()
{
    [[NativeXCore instance] showCachedInterstitial];
}

void uShowInterstitial()
{
    [[NativeXCore instance] showInterstitial];
}

void uShowInterstitialFromPoint(float x, float y)
{
    [[NativeXCore instance] showInterstitialFromPoint:CGPointMake(x, y)];
}

void uShowBanner(float x, float y, float width, float height)
{
    [[NativeXCore instance] showBannerWithRect:CGRectMake(x, y, width, height)];
}

void uRemoveBanner()
{
    [[NativeXCore instance] removeBanner];
}

void uConnectWithAppId(const char *appId)
{
    [[NativeXCore instance] connectWithAppId:GetStringParamOrNil(appId) ];
}

void uActionTakenWithActionId(const char *actionId)
{
    [[NativeXCore instance]actionTaken:GetStringParamOrNil(actionId)];
}


void uRedeemCurrency()
{
    [[NativeXCore instance] redeemCurrency];
}

void uTrackInAppPurchase(const char *storeProdId, const char *storeTransId, float cost, int quantity, const char *prodTitle)
{
    NativeXInAppPurchaseTrackRecord *myRecord = [[NativeXInAppPurchaseTrackRecord alloc] init];
    
    myRecord.storeProductID = GetStringParam(storeProdId);
    myRecord.storeTransactionID = GetStringParam(storeTransId);
    myRecord.costPerItem = [[NSDecimalNumber alloc] initWithFloat:cost];
    myRecord.quantity = quantity;
    myRecord.productTitle = GetStringParam(prodTitle);
    myRecord.currencyLocale = [NSLocale currentLocale];
    myRecord.storeTransactionTime = [NSDate date];
    
    [[NativeXCore instance]trackInAppPurchase:myRecord];
}

void uClose()
{
    [[NativeXCore instance] close];
}